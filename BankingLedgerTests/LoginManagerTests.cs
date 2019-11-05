using System;
using BankingLedger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingLedgerTests
{
  [TestClass]
  public class LoginManagerTests
  {
    #region CreateNewAccount Tests
    [TestMethod]
    public void CreateNewAccount_ValidNewUser_LoginSucceeds()
    {
      LoginManager.CreateNewAccount("First", "Last", "Username", "Password");
      Assert.IsTrue(LoginManager.LogIn("Username", "Password"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CreateNewAccount_NullFirstName_ExceptionThrown()
    {
      LoginManager.CreateNewAccount(null, "Last name", "Username", "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CreateNewAccount_NullLastName_ExceptionThrown()
    {
      LoginManager.CreateNewAccount("First name", null, "Username", "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CreateNewAccount_NullUsername_ExceptionThrown()
    {
      LoginManager.CreateNewAccount("First name", "Last name", null, "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CreateNewAccount_NullPassword_ExceptionThrown()
    {
      LoginManager.CreateNewAccount("First name", "Last name", "Username", null);
    }
    #endregion

    #region LogIn Tests
    [TestMethod]
    public void LogIn_ValidUser_UserIsCurrentUser()
    {
      LoginManager.CreateNewAccount("TestName", "TestLast", "TestUsername", "TestPassword");
      LoginManager.LogIn("TestUsername", "TestPassword");
      Assert.AreEqual("TestName", LoginManager.CurrentUser.FirstName);
    }

    [TestMethod]
    public void LogIn_InvalidUser_UserIsNotCurrentUser()
    {
      LoginManager.LogIn("SomeInvalidUsername", "AnyPassword");
      Assert.AreNotEqual("SomeInvalidUsername", LoginManager.CurrentUser.FirstName);
    }
    #endregion

    #region LogOut Tests
    [TestMethod]
    public void LogOut_CurrentUserIsNoUser()
    {
      LoginManager.CreateNewAccount("TestName", "TestLast", "TestUsername", "TestPassword");
      LoginManager.LogIn("TestUsername", "TestPassword");
      Assert.AreEqual("TestName", LoginManager.CurrentUser.FirstName);
      LoginManager.LogOut();
      Assert.AreEqual(0, LoginManager.CurrentUser.Id);
    }
    #endregion

    #region UserExists Tests
    [TestMethod]
    public void UserExists_ValidUser_UserFound()
    {
      LoginManager.CreateNewAccount("Exist", "Ing", "User12345", "pass");
      Assert.IsTrue(LoginManager.UserExists("User12345"));
    }

    [TestMethod]
    public void UserExists_UserDoesntExist_UserNotFound()
    {
      Assert.IsFalse(LoginManager.UserExists("SomeUsernameNeverCreated"));
    }
    #endregion
  }
}
