using NBC;

namespace Test;

public class NBCTests {
    INBC nbc;

    [SetUp]
    public void Setup() {
        nbc = new NBC.NBC();
        nbc.LoadData(@"ML.CSV");
    }

    [Test]
    public void Total_Output_Count() {
        const int totalOutputNumber = 2;
        Dictionary<string, double> outputs = nbc.Compute("Sunny,Cool,High,true");

        Assert.AreEqual(totalOutputNumber, outputs.Count);
    }

    [Test]
    public void Is_Contains_Yes_Or_No() {
        Dictionary<string, double> outputs = nbc.Compute("Sunny,Cool,High,true");

        Assert.IsTrue(outputs.ContainsKey("Yes"));
        Assert.IsTrue(outputs.ContainsKey("No"));
    }

    [Test]
    public void Test_algoritme() {
        const double No = 0.9408;
        const double Yes = 0.2419753086419753;
        
        Dictionary<string, double> outputs = nbc.Compute("Sunny,Cool,High,true");

        Assert.AreEqual(No, outputs["No"]);
        Assert.AreEqual(Yes, outputs["Yes"]);
    }

    [Test]
    public void Get_Exception_For_Missing_Some_Input() {
        Assert.Throws<MissingSomeException>(() => nbc.Compute("Cool,High,true"));
    }

    [Test]
    public void Get_Exception_For_Wrong_Input() {
        Assert.Throws<WrongInputException>(() => nbc.Compute("Cool,High,true,Sunny"));
    }
}