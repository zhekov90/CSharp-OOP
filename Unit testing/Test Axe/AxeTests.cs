using NUnit.Framework;

[TestFixture]
public class AxeTests
{

    private Dummy target;

    [SetUp]
    public void SetDummy()
    {
        this.target = new Dummy(100, 500);
    }

    [Test]
    public void AxeShouldLoseDurabilityAfterAttack()
    {
        //arrange
        var axe = new Axe(10, 5);
       

        //act 
        axe.Attack(this.target);

        //assert
        Assert.AreEqual(4, axe.DurabilityPoints, "Axe loses durability after attack");

    }
    [Test]
    public void AxeShouldThrowExceptionIfAnAttackIsMadeWithBrokenWeapon()
    {
        //arrange

        var axe = new Axe(1, 1);

        //act 
        axe.Attack(this.target);

        //assert
        Assert.That(() => axe.Attack(this.target), Throws.InvalidOperationException.With.Message.EqualTo("Axe is broken."));

    }
}