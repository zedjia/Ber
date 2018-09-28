using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WC.Lib
{

    public static class AppLog
    {
       
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        public static void WriteLog(string logInfo, string type = "Listenplaywindow")
        {
            string m_Logfilename = string.Format(@"{0}-{1}.log", Dns.GetHostName(), DateTime.Now.ToShortDateString());
            m_Logfilename = m_Logfilename.Replace("/", "-");
            string m_logfilepath = Application.StartupPath + "\\Log\\" + type;
            if (!Directory.Exists(m_logfilepath))
            {
                Directory.CreateDirectory(m_logfilepath);
            }

            if (!File.Exists(m_logfilepath + "\\" + m_Logfilename))
            {
                FileStream fs = null;
                try
                {
                    fs = File.Create(m_logfilepath + "\\" + m_Logfilename);

                }
                catch
                { }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }


            StreamWriter m_Filewrite = null;
            try
            {
                m_Filewrite = new StreamWriter(m_logfilepath + "\\" + m_Logfilename, true);
                m_Filewrite.WriteLine(DateTime.Now.ToString() + "\t" + logInfo);

            }
            catch
            { }
            finally
            {
                if (m_Filewrite != null)
                {
                    m_Filewrite.Flush();
                    m_Filewrite.Close();
                }
            }
        }
        public static void WriteLog2(string logInfo, string type = "startcheckinfo")
        {
            DateTime now = DateTime.Now;
            string m_Logfilename = string.Format(@"{0}-{1}.log", Dns.GetHostName(), now.ToString("yyyy-MM-dd HH-mm-ss"));
            m_Logfilename = m_Logfilename.Replace("/", "-");
            string m_logfilepath = Application.StartupPath + "\\Log\\" + type;
            if (!Directory.Exists(m_logfilepath))
            {
                Directory.CreateDirectory(m_logfilepath);
            }

            if (!File.Exists(m_logfilepath + "\\" + m_Logfilename))
            {
                FileStream fs = null;
                try
                {
                    fs = File.Create(m_logfilepath + "\\" + m_Logfilename);

                }
                catch
                { }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }


            StreamWriter m_Filewrite = null;
            try
            {
                m_Filewrite = new StreamWriter(m_logfilepath + "\\" + m_Logfilename, true);
                m_Filewrite.WriteLine(logInfo);

            }
            catch
            { }
            finally
            {
                if (m_Filewrite != null)
                {
                    m_Filewrite.Flush();
                    m_Filewrite.Close();
                }
            }
        }
        public static void WriteLogselect(string logInfo, string type = "selectplay")
        {
            DateTime now = DateTime.Now;
            string m_Logfilename = string.Format(@"{0}-{1}.log", Dns.GetHostName(), now.ToShortDateString());
            m_Logfilename = m_Logfilename.Replace("/", "-");
            string m_logfilepath = Application.StartupPath + "\\Log\\" + type;
            if (!Directory.Exists(m_logfilepath))
            {
                Directory.CreateDirectory(m_logfilepath);
            }

            if (!File.Exists(m_logfilepath + "\\" + m_Logfilename))
            {
                FileStream fs = null;
                try
                {
                    fs = File.Create(m_logfilepath + "\\" + m_Logfilename);

                }
                catch
                { }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }


            StreamWriter m_Filewrite = null;
            try
            {
                m_Filewrite = new StreamWriter(m_logfilepath + "\\" + m_Logfilename, true);
                m_Filewrite.WriteLine(DateTime.Now.ToString() + "\t" + logInfo);

            }
            catch
            { }
            finally
            {
                if (m_Filewrite != null)
                {
                    m_Filewrite.Flush();
                    m_Filewrite.Close();
                }
            }
        }

    }
}
