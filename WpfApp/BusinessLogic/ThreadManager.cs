﻿using DataAccess;

namespace BusinessLogic
{
    public class ThreadManager
    {
        private int _threadCount;
        private List<Task> _tasks;
        private CancellationTokenSource _cts;
        private Action<GeneratedData> _updateUI;
        private RandomStringGenerator _stringGenerator;

        public ThreadManager(int threadCount, Action<GeneratedData> updateUI)
        {
            _threadCount = threadCount;
            _updateUI = updateUI;
            _tasks = new List<Task>();
            _stringGenerator = new RandomStringGenerator();
        }

        public void Start()
        {
            try
            {
                _cts = new CancellationTokenSource();

                for (int i = 1; i <= _threadCount; i++)
                {
                    int threadId = i;
                    _tasks.Add(Task.Run(() => GenerateData(threadId, _cts.Token)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting threads: {ex.Message}");
            }
        }

        public async Task Stop()
        {
            try
            {
                _cts.Cancel();
                await Task.WhenAll(_tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping threads: {ex.Message}");
            }
        }

        private async Task GenerateData(int threadId, CancellationToken token)
        {
            var dbContext = new DataContext();
            var random = new Random();

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(random.Next(500, 2000), token);
                    string data = _stringGenerator.Generate();
                    var generatedData = new GeneratedData
                    {
                        ThreadID = threadId,
                        Time = DateTime.Now,
                        Data = data
                    };

                    dbContext.GeneratedData.Add(generatedData);
                    await dbContext.SaveChangesAsync();

                    _updateUI(generatedData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating data for thread {threadId}: {ex.Message}");
                }
            }
        }
    }
}
