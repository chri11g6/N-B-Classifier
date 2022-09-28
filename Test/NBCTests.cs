using NBC;

namespace Test;

public class NBCTests {
    INBC nbc;

    // [SetUp]
    // public void Setup() {
    // }

    [Test]
    public void Total_Output_Count() {
        const int totalOutputNumber = 2;
        Dictionary<string, double> outputs = nbc.Compute("Sunny,Cool,High,true");

        Assert.AreEqual(totalOutputNumber, outputs.Count);
    }

    [Test]
    public void Get_Exception_For_Missing_Some_Input() {
        Assert.Throws<Exception>(() => nbc.Compute("Cool,High,true"));
    }

    [Test]
    public void Get_Exception_For_Wrong_Input() {
        Assert.Throws<Exception>(() => nbc.Compute("Cool,High,true,Sunny"));
    }
}