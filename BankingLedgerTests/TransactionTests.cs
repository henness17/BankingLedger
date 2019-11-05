using System;
using BankingLedger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingLedgerTests
{
  [TestClass]
  public class TransactionTests
  {
    #region Constructor Tests
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Transaction_AmountTooLarge_ExceptionThrown()
    {
      Transaction largeTransaction = new Transaction(0, 0, Transaction.TransactionType.Deposit, 21474836.48);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Transaction_AmountTooSmall_ExceptionThrown()
    {
      Transaction smallTransaction = new Transaction(0, 0, Transaction.TransactionType.Deposit, 0);
    }
    #endregion
  }
}
