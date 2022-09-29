using NBC.data;
using NBC.data.database;

namespace Test;

public class NBCTableTests {
	INBCTable nbcDatabase;

    [SetUp]
    public void Setup() {
        nbcDatabase = new NBCTable();

		// col 1
		nbcDatabase.AddRowData("kage", null, 0.3, 1);
		nbcDatabase.AddRowData("isbil", null, 0.5, 1);
		nbcDatabase.AddRowData("ged", null, 0.7, 1);
		// col 2
		nbcDatabase.AddRowData("ja", null, 0.69, 2);
		nbcDatabase.AddRowData("nej", null, 0.99, 2);
    }

    [Test]
    public void Total_Input() {
        const int totalInput = 2;

        Assert.AreEqual(totalInput, nbcDatabase.GetConutOfInput());
    }

    [TestCase("kage", 1, ExpectedResult = true)]
    [TestCase("isbil", 1, ExpectedResult = true)]
    [TestCase("ged", 1, ExpectedResult = true)]
    [TestCase("ja", 2, ExpectedResult = true)]
    [TestCase("nej", 2, ExpectedResult = true)]
    [TestCase("kage", 2, ExpectedResult = false)]
    [TestCase("isbil", 2, ExpectedResult = false)]
    [TestCase("ged", 2, ExpectedResult = false)]
    [TestCase("ja", 1, ExpectedResult = false)]
    [TestCase("nej", 1, ExpectedResult = false)]
    public bool Test_Data_Exist(string data, int col) {
		return nbcDatabase.DataExistInCol(data, col);
    }
}