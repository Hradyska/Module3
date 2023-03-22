using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3
{
    internal class SomeAsyncMethods
    {
        public static event Func<List<int>, Task<List<int>>> Run;
        public static async Task<List<int>> MethodOne(List<int> list)
        {
            Task<List<int>> task = Task.FromResult(list.Distinct().ToList());
            await task;
            var distinctList = task.Result;
            _ = Task.Delay(3000);
            return distinctList;
        }

        public static async Task<List<int>> MethodTwo(List<int> list)
        {
            Task<List<int>> task = Task.FromResult(list.Take(3).ToList());

            await task;
            var taketList = task.Result;
            _ = Task.Delay(3000);
            return taketList;
        }

        public static async Task<List<int>> MethodThree(List<int> list)
        {
            Task<List<int>> task = Task.FromResult(list.Where(x => x % 2 == 0).Where(x => x > 10).Order().ToList());
            await task;
            var taketList = task.Result;
            _ = Task.Delay(3000);
            return taketList;
        }

        public static async Task<List<int>> OnRun(List<int> list)
        {
            Func<List<int>, Task<List<int>>> handler = Run;

            if (handler == null)
            {
                return null;
            }

            Delegate[] invocationList = handler.GetInvocationList();
            List<int> result = new ();
            foreach (Delegate @delegate in invocationList)
            {
                Task<List<int>> task = (Task<List<int>>)@delegate.DynamicInvoke(list);
                await task;
                result = result.Concat(task.Result).ToList();
            }

            return result;
        }
    }
}
