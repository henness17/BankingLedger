using System;
using BankingLedger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingLedgerTests
{
  [TestClass]
  public class TransactionManagerTests
  {
    #region Deposit Tests
    [TestMethod]
    public void Deposit_ValidDeposit_BalanceIsCorrect()
    {
      this.SetupNewUser("SomeUser39248", "password");
      Assert.AreEqual(0, LoginManager.CurrentUser.Balance);
      TransactionManager.Deposit(100);
      Assert.AreEqual(10000, LoginManager.CurrentUser.Balance);
    }
    #endregion

    #region Withdraw Tests
    [TestMethod]
    public void Withdraw_ValidWithdrawal_BalanceIsCorrect()
    {
      this.SetupNewUser("SomeUsername342893", "password2");
      Assert.AreEqual(0, LoginManager.CurrentUser.Balance);
      TransactionManager.Deposit(100);
      Assert.AreEqual(10000, LoginManager.CurrentUser.Balance);
      TransactionManager.Withdraw(50);
      Assert.AreEqual(5000, LoginManager.CurrentUser.Balance);
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
