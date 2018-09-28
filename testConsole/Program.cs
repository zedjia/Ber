using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //string data =
            //    "{\"buy\":[[1.803,4703.406],[1.7778,50.6252],[1.7777,5000],[1.7773,50.638],[1.777,100]],\"sell\":[[1.805,34831.4434,34831.4434],[1.9155,1000,219343.633],[1.92,16562.5859,235906.2189],[1.922,52.0291,235958.248],[1.928,5000,240958.248]]}";

            ////
            //var obj = JsonConvert.DeserializeObject(data);
            //Trades trad = JsonConvert.DeserializeObject<Trades>(data);

            //data =
            //    "{\"max\":2.1111,\"min\":1.65,\"sum\":73004530.2583,\"d\":[[\"00:26:10\",1.8001,1425.867,\"sell\",\"2017 - 08 - 18\"],[\"00:26:04\",1.8061,2661.6508,\"buy\",\"2017 - 08 - 18\"]]}";

            //obj = JsonConvert.DeserializeObject(data);
            //Order order = JsonConvert.DeserializeObject<Order>(data);

            //Console.WriteLine("差集测试");

            //List<int> list1=new List<int>(){1,2,3,4,5};
            //List<int> list2 = new List<int>() { 3, 4,5,6,7,8,9 };
            //var list3= list2.Except(list1);



            //Console.WriteLine("差集测试 END");



            //Console.WriteLine("主线程启动");
            ////ThreadPool.QueueUserWorkItem(StartCode,5);
            //Task.Run(StartCode);
            //Task.Delay(1000);
            //Console.WriteLine("主线程运行到此！");


            //Console.WriteLine("主线程测试开始..");
            //AsyncMethod();
            //Thread.Sleep(1000);
            //Console.WriteLine("主线程测试结束..");
            //Console.ReadLine();
            /// 
            /// 
            /// 
            //MainExceptionAsync();

            //var t = DoExceptionAsync();
            //t.Wait();
            //Console.WriteLine($"{nameof(t.Status)}: {t.Status}"); //任务状态
            //Console.WriteLine($"{nameof(t.IsCompleted)}: {t.IsCompleted}"); //任务完成状态标识
            //Console.WriteLine($"{nameof(t.IsFaulted)}: {t.IsFaulted}"); //任务是否有未处理的异常标识


            const int num = 1000000;
            var t = Yield1000(num);

            Loop(num / 10);
            //Loop(num / 10);
            //Loop(num / 10);
            Console.WriteLine($"Sum: {t.Result}");


            //Console.WriteLine($"{DateTime.Now.ToString("T")} - Starting");
            //var t1 = ExecuteAsync(() => Thread.Sleep(5000),"1");
            //var t2 = ExecuteAsync(() => Thread.Sleep(1000),"2");
            //var t3 = ExecuteAsync(() => Thread.Sleep(3000),"3");

            //Task.WaitAll(t1, t2, t3);
            //Console.WriteLine($"{DateTime.Now.ToString("T")} - Finished");
            //Console.ReadKey();


            Console.ReadKey();
        }
        #region 实例方法

        private static async Task ExecuteAsync(Action action,string id)
        {
            Console.WriteLine($"开始执行ExecuteAsync-{id}");
            // Execute the continuation asynchronously
            await Task.Yield();  // The current thread returns immediately to the caller
                                 // of this method and the rest of the code in this method
                                 // will be executed asynchronously
            Console.WriteLine($"开始执行ExecuteAsyncaction-{id}");
            action();
            Console.WriteLine($"结束action-{id}");

            Console.WriteLine($"{DateTime.Now.ToString("T")} -{id} - Completed task on thread {Thread.CurrentThread.ManagedThreadId}");
        }



        //static async Task AsyncDemo1(int num)
        //{
        //    for (var i = 0; i < num; i++)
        //    {
        //        if (i % 1000 == 0)
        //        {
        //            Console.WriteLine($"AsyncDemo1:{i}");
        //        }
        //    }
        //}


        /// <summary>
        /// 循环
        /// </summary>
        /// <param name="num"></param>
        private static void Loop(int num)
        {
            for (var i = 0; i < num; i++)
            {
                if (i % 100 == 0)
                {
                    ///Thread.Sleep(1000);
                    Console.WriteLine($"Loop:{i}");
                }
            }
        }

        public static async Task<int> Yield1000(int n)
        {
            var sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += i;
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"Yield1000: {i}");
                    await Task.Delay(100);
                    //await Task.Yield(); //创建异步产生当前上下文的等待任务
                }
            }
            return sum;
        }


        private static async Task MainExceptionAsync()
        {
            try
            {
                DoExceptionAsync2();
                //await Task.Run(() => { DoExceptionAsync2(); });

                //Task.Run(() => { throw new Exception(); });
            }
            catch (Exception)
            {
                Console.WriteLine($"{nameof(MainExceptionAsync)} 出现异常！");
            }
        }

        private static async Task DoExceptionAsync2()
        {
            //try
            //{
                throw new Exception();
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine($"{nameof(DoExceptionAsync2)} 出现异常！");
            //}
        }


        private static async Task DoExceptionAsync()
        {
            try
            {
                await Task.Run(() => { throw new Exception(); });
            }
            catch (Exception)
            {
                Console.WriteLine($"{nameof(DoExceptionAsync)} 出现异常！");
            }
        }



        private static async Task StartCode()
        {
            Console.WriteLine("準備開始子线程！");
            await Task.Delay(3000);
            Console.WriteLine("子线程延时结束，准备循环！");
            while (true)
            {
                await Task.Delay(3000); //模拟代码操作    
                Console.WriteLine("开始执行子线程...{0}", Task.CurrentId);
            }

        }


        static async void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");
            for (int i = 0; i < 2; i++)
            {
                MyMethod2("MyMethod2["+i);
                MyMethod("MyMethod[" + i);
            }
            
            Console.WriteLine("异步代码执行完毕");
        }

        static async void MyMethod(string Id)
        {
            for (int i = 0; i < 3; i++)
            {
                
                Console.WriteLine("Id:"+ Task.CurrentId + "-"+ Id + "-异步执行" + i.ToString() + "..");
                await Task.Delay(1000); //模拟耗时操作
            }
            Console.WriteLine("MyMethod 执行完毕");
        }
        static async void MyMethod2(string id)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Id:" + Task.CurrentId + "-" + id + "-异步执行" + i.ToString() + "..");
                //await Task.Delay(1000); //模拟耗时操作
            }
            Console.WriteLine("MyMethod2 执行完毕");
        }


        #endregion


    }

   

    public class Trades
    {
        public decimal[,] buy { get; set; }

        public decimal[][] sell { get; set; }

    }

    public class Order
    {
        public decimal max { get; set; }

        public decimal min { get; set; }

        public decimal sum { get; set; }

        public decimal volume { get; set; }

        
        public string[][] d{ get; set; }
    }

    public class OrderDetails
    {
        public DateTime time { get; set; }

        public bool type { get; set; }

        public decimal price { get; set; }

        public decimal amount { get; set; }

        public decimal money { get; set; }
    }

}
