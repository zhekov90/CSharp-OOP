// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class StageTests
    {
        [Test]
        public void ctorStageShouldWork()
        {
            Performer performer = new Performer("ivan", "ivanov", 20);
            Stage stage = new Stage();
            stage.AddPerformer(performer);
            Assert.AreEqual(1, stage.Performers.Count);
        }
        [Test]
        public void performerUnder18()
        {
            Performer performer = new Performer("ivan", "ivanov", 17);
            Stage stage = new Stage();

            Assert.Throws<ArgumentException>(() =>
            {
                stage.AddPerformer(performer);
            });
        }
        [Test]
        public void addCorrectPerformer()
        {
            Performer performer = new Performer("ivan", "ivanov", 20);
            Stage stage = new Stage();
            stage.AddPerformer(performer); Assert.AreEqual("ivan ivanov", performer.FullName);
        }
        [Test]
        public void addSongShouldWork()
        {
            Song song = new Song("asd", new TimeSpan(0, 2, 30));
            Assert.AreEqual(song.Duration, new TimeSpan(0, 2, 30));
            Assert.AreEqual(song.Name, "asd");
        }
        [Test]
        public void SongWithout1MinuteDuration()
        {
            Performer performer = new Performer("ivan", "ivanov", 20);
            Stage stage = new Stage();
            Song song = new Song("asd", new TimeSpan(0, 0, 30));

            Assert.Throws<ArgumentException>(() =>
            {
                stage.AddSong(song);
            });
        }

        [Test]
        public void AddPerformerShouldWork()
        {
            Performer performer = new Performer("ivan", "ivanov", 20);
            Stage stage = new Stage();

            stage.AddPerformer(performer);
            Assert.AreEqual(stage.Performers.Count, 1);

        }

        [Test]
        public void AddNullPerformer()
        {
            Stage stage = new Stage();
            Assert.Throws<ArgumentNullException>(() =>
            {
                stage.AddPerformer(null);
            });

        }
        [Test]
        public void dobavqnePesenKumStage()
        {
            Stage stage = new Stage();
            Song song = new Song("asd", new TimeSpan(0, 2, 30));
            Assert.DoesNotThrow(() =>
            {
                stage.AddSong(song);
            });
        }
        [Test]
        public void AddNullSongToStage()
        {
            Stage stage = new Stage();

            Assert.Throws<ArgumentNullException>(() =>
            {
                stage.AddSong(null);
            });
        }
        [Test]
        public void songtostage()
        {
            Stage stage = new Stage();
            Song song = new Song("asd", new TimeSpan(0, 2, 30));
            stage.AddSong(song);
            stage.GetSong(song.Name);
            Assert.AreEqual(song.Name, "asd");
        }

        [Test]
        public void AddNullSongright()
        {
            Stage stage = new Stage();
            Song song = new Song("asd", new TimeSpan(0, 2, 30));
            stage.AddSong(song);
            Assert.AreEqual(song.Duration, new TimeSpan(0, 2, 30));
        }

        [Test]
        public void performertostage()
        {
            Stage stage = new Stage();

            Performer performer = new Performer("ivan", "ivanov", 20);
            stage.GetPerformer(performer.FullName);
            
            Assert.AreEqual("ivan ivanov", performer.FullName);
        }

    }
}