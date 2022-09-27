using NBC;

namespace Test;

public class NBCTests {
    INBC nbc;

    // [SetUp]
    // public void Setup() {
    // }

    [Test]
    public void TotalOutput() {
        const int totalOutputNumber = 2;
        Dictionary<string, double> outputs = nbc.Compute("Sunny,Cool,High,true");

        Assert.AreEqual(totalOutputNumber, outputs.Count);
    }
}