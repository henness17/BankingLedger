using System;
using BankingLedger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingLedgerTests
{
  [TestClass]
  public class UserTests
  {
    #region Constructor Tests
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void User_NullFirstName_ExceptionThrown()
    {
      User user = new User(0, null, "Last name", "Username", "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void User_NullLastName_ExceptionThrown()
    {
      User user = new User(0, "First name", null, "Username", "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void User_NullUsername_ExceptionThrown()
    {
      User user = new User(0, "First name", "Last name", null, "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void User_NullPassword_ExceptionThrown()
    {
      User user = new User(0, "First name", "Last name", "Username", null);
    }
    #endregion

    #region MakeTransaction Tests
    [TestMethod]
    public void MakeTransaction_ValidDeposit_BalanceIsCorrect()
    {
      this.SetupNewUser("TestUser2020202", "password3289");
      Assert.AreEqual(0, LoginManager.CurrentUser.Balance);
      Transaction firstDeposit = new Transaction(
        0, 
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Deposit, 
        1);
      LoginManager.CurrentUser.MakeTransaction(firstDeposit);
      Assert.AreEqual(100, LoginManager.CurrentUser.Balance);
      Transaction secondDeposit = new Transaction(
        0, 
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Deposit, 
        1.50);
      LoginManager.CurrentUser.MakeTransaction(secondDeposit);
      Assert.AreEqual(250, LoginManager.CurrentUser.Balance);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void MakeTransaction_DepositNullAmount_ExceptionThrown()
    {
      Transaction deposit = null;
      LoginManager.CurrentUser.MakeTransaction(deposit);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void MakeTransaction_DepositBalanceTooLarge_ExceptionThrown()
    {
      Transaction maxDeposit = new Transaction(
        0, 
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Deposit, 
        21474836);
      LoginManager.CurrentUser.MakeTransaction(maxDeposit);
      LoginManager.CurrentUser.MakeTransaction(maxDeposit);
    }

    [TestMethod]
    public void MakeTransaction_ValidWithdrawal_BalanceIsCorrect()
    {
      this.SetupNewUser("TestUser23849", "password39");
      Assert.AreEqual(0, LoginManager.CurrentUser.Balance);
      Transaction firstDeposit = new Transaction(
        0, 
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Deposit, 
        1);
      LoginManager.CurrentUser.MakeTransaction(firstDeposit);
      Assert.AreEqual(100, LoginManager.CurrentUser.Balance);
      Transaction secondDeposit = new Transaction(
        0, 
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Withdrawal, 
        0.50);
      LoginManager.CurrentUser.MakeTransaction(secondDeposit);
      Assert.AreEqual(50, LoginManager.CurrentUser.Balance);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void MakeTransaction_WithdrawalBalanceTooSmall_ExceptionThrown()
    {
      this.SetupNewUser("User43289", "somePassword");
      Assert.AreEqual(0, LoginManager.CurrentUser.Balance);
      Transaction firstDeposit = new Transaction(
        0, 
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Deposit, 
        1);
      LoginManager.CurrentUser.MakeTransaction(firstDeposit);
      Assert.AreEqual(100, LoginManager.CurrentUser.Balance);
      Transaction secondDeposit = new Transaction(
        0, 
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Withdrawal, 
        1.50);
      LoginManager.CurrentUser.MakeTransaction(secondDeposit);
    }
    #endregion

    #region Helper Methods
    private void SetupNewUser(string username, string password)
    {
      LoginManager.LogOut();
      Random random = new Random();
      User testUser = new User(
        random.Next(int.MaxValue),
        "Test",
        "User",
        username,
        password);
      LoginManager.CreateNewAccount(
        testUser.FirstName,
        testUser.LastName,
        testUser.Username,
        testUser.Password);
      LoginManager.LogIn(testUser.Username, testUser.Password);
    }
    #endregion
  }
}
