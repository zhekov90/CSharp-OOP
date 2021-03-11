using NUnit.Framework;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

[TestFixture]
public class DummyTests
{
    private const int Experience = 200;
    private Dummy target;
    [SetUp]
    public void SetDummy()
    {
        this.target = new Dummy(100, 500);
    }
    [Test]
    public void DummyLosesHealthIfAttacked()
    {
        //arrange
        var axe = new Axe(50, 5);

        //act
        target.TakeAttack(50);

        //assert
        Assert.AreEqual(50, target.Health);
        //Assert.That(() => axe.Attack(dummy), Throws.InvalidOperationException);
    }

    [Test]
    public void DummyThrowsExceptionIfAttackedAndItsWithoutHealth()
    {
        //arrange
        var target = new Dummy(0, 5);



        //assert
        Assert.That(() => target.TakeAttack(30), //act
            Throws.InvalidOperationException.With.Message.EqualTo("Dummy is dead."), "Dead targets cannot be attacked.");
    }

    [Test]
    public void DummyCanGiveXp()
    {
        //arrange
        var target = new Dummy(0, Experience);

        //act
        var experience = target.GiveExperience();

        //assert
        Assert.That(experience, Is.EqualTo(Experience));
    }
    [Test]
    public void DummyCanNotGiveXpIfAlive()
    {

        //assert
        Assert.That(() => target.GiveExperience(), //act
         Throws.InvalidOperationException.With.Message.EqualTo("Target is not dead."));
    }
}
