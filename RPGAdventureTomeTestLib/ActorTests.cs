using RPGAdventureTome.Actors;
using NUnit.Framework;
using RPGAdventureTomeTestLib.Utils;
using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace AdventureTomeTestLib
{
    [TestFixture(Author = "SupCMDr", Description = "Actor Creation Tests")]
    class ActorTests
    {
        private DataLoader loader;
        private ILogger logger;

        private LoggingConfiguration config;
        private FileTarget logfile;
        private ConsoleTarget logconsole;

        [SetUp]
        public void Setup()
        {
            loader = new DataLoader();
            logger = LogManager.GetCurrentClassLogger();
        }

        [Test]
        public void CreateBreedsTest()
        {
            int health = 10;
            int attack = 3;

            Breed newBreed = new Breed("Orc", health, attack);

            Assert.NotNull(newBreed, "The new breed was not created.");
        }

        [Test]
        public void LoadBreedsTest()
        {
            List<Breed> breeds = loader.LoadMonsterBreeds();

            Assert.IsNotEmpty(breeds);
            Assert.GreaterOrEqual(breeds.Count, 1);


            logger.Info("TEST: " + TestContext.CurrentContext.Test.Name);
            foreach(Breed breed in breeds){
                logger.Info("Breed: " + breed.name);
                if(breed.parent != null)
                    logger.Info("Parent:  " + breed.parent.name);
                logger.Info("Health: " + breed.health);
                logger.Info("Attack: " + breed.attack);
                logger.Info("");
            }
        }

        [Test]
        public void CloneMonsterTest()
        {
            List<Breed> breeds = loader.LoadMonsterBreeds();

            List<Monster> clones = new List<Monster>();

            foreach(Breed breed in breeds)
            {
                clones.Add(breed.newMonster());
            }

            int index = 0;
            foreach(Monster clone in clones)
            {
                Assert.AreEqual(clone.breed, breeds[index]);
                index++;
            }
        }
    }
}
