using BusinessLogic;

namespace UnitTests
{
    public class ThreadManagerTests
    {
        [Fact]
        public async Task ThreadManager_GeneratesData()
        {
            bool dataGenerated = false;
            var threadManager = new ThreadManager(2, (data) => dataGenerated = true);

            threadManager.Start();
            Task.Delay(3000).Wait();
            await threadManager.Stop();

            Assert.True(dataGenerated);
        }
    }
}