using static Module3.SomeAsyncMethods;
namespace Module3
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            List<int> list1 = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                list1.Add(rand.Next(0, 100));
            }

            Run += MethodOne;
            Run += MethodTwo;
            Run += MethodThree;
            var res = await OnRun(list1);
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}