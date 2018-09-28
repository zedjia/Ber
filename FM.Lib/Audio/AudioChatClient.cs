using System;
using System.Drawing;
using FM.Lib.Model;

namespace FM.Lib.Audio
{
    public class AudioChatClient
    {
        public AudioChatClient(ConfigEntity configEntity)//,UserDto dto
        {
            ConfigEntity = configEntity;
            //CurrentUser = dto;//todo: 实际需要解除注释
            //CurrentUser=new UserDto(){UserName = "user"+DateTime.Now.ToString("mmssfff")};
        }
        ~AudioChatClient()
        {
            DisconnectOMCS();
        }

        public Action<string> DisplayMessage;

        //public UserDto CurrentUser { get; set; }
        public ConfigEntity ConfigEntity { get; set; }


        private IMultimediaManager multimediaManager;
        private IChatGroup chatGroup;


        public void StartOMCSClient(UserDto CurrentUser,string roomId)
        {
            try
            {

                multimediaManager = MultimediaManagerFactory.GetSingleton();
                multimediaManager.CameraVideoSize = new Size(320, 240);
                multimediaManager.AutoAdjustCameraEncodeQuality = false;
                multimediaManager.ChannelMode = ChannelMode.P2PDisabled;
                multimediaManager.SecurityLogEnabled = false;
                multimediaManager.CameraDeviceIndex = 0;
                multimediaManager.MicrophoneDeviceIndex =ConfigEntity.MicrophoneIndex;
                multimediaManager.SpeakerIndex = ConfigEntity.SpeakerIndex;
                multimediaManager.DesktopEncodeQuality = 3;
                multimediaManager.AutoReconnect = true;
                multimediaManager.Initialize(CurrentUser.UserName, "", ConfigEntity.SocketServerIp, ConfigEntity.AudioPort);


                this.chatGroup = this.multimediaManager.ChatGroupEntrance.Join(ChatType.Audio, roomId);
                this.chatGroup.SomeoneJoin += chatGroup_SomeoneJoin;
                this.chatGroup.SomeoneExit += chatGroup_SomeoneExit;
                foreach (IChatUnit unit in this.chatGroup.GetOtherMembers())
                {
                    unit.MicrophoneConnector.BeginConnect(unit.MemberID);
                }

                if (multimediaManager.Available)
                {
                    DisplayMsg("语音服务已经连接成功.");
                }
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message);
            }
        }

        public void DisconnectOMCS()
        {
            try
            {
                if (this.multimediaManager != null)
                {
                    //this.multimediaManager.AudioCaptured -= new ESBasic.CbGeneric<byte[]>(multimediaManager_AudioCaptured);
                    //this.multimediaManager.AudioPlayed -= new ESBasic.CbGeneric<byte[]>(multimediaManager_AudioPlayed);
                    if (this.chatGroup != null)
                    {
                        //退出聊天室
                        this.multimediaManager.ChatGroupEntrance.Exit(ChatType.Audio, this.chatGroup.GroupID);
                    }
                    multimediaManager.Dispose();
                }
                DisplayMsg("语音服务已经断开.");
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }



        void DisplayMsg(string msg)
        {
            if (DisplayMessage != null)
            {
                DisplayMessage(msg);
            }
        }




        void chatGroup_SomeoneExit(string memberID)
        {

            //Task.Factory.StartNew(() => this.chatGroup_SomeoneExit(memberID));

        }

        //当有人加入聊天室
        void chatGroup_SomeoneJoin(IChatUnit unit)
        {
            //Task.Factory.StartNew(() => this.chatGroup_SomeoneJoin(unit));

            unit.MicrophoneConnector.BeginConnect(unit.MemberID);
        }




    }

}
