using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Recodme.RD.BoraNow.BusinessLayer.OperationResults;

namespace Recodme.RD.BoraNow.BusinessLayer.Base
{
    public class BaseBusinessObject
    {
        private TransactionOptions opts = new TransactionOptions()
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };
        protected OperationResult ExecuteTransaction(Action operation)
        {
            var scope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                operation.Invoke();
                scope.Complete();
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        protected OperationResult<TR> ExecuteTransaction<TR>(Func<TR> operation)
        {
            var transactionScope = new TransactionScope(TransactionScopeOption.Required, opts, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var res = operation.Invoke();
                transactionScope.Complete();
                return new OperationResult<TR>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<TR>() { Success = false, Exception = e };
            }
        }
        protected OperationResult ExecuteOperation(Action operation)
        {
            try
            {
                operation.Invoke();
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        protected async Task<OperationResult> ExecuteOperationAsync(Func<Task> operation)
        {
            try
            {
                await operation.Invoke();
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        protected OperationResult<TR> ExecuteOperation<TR>(Func<TR> operation)
        {
            try
            {
                var res = operation.Invoke();
                return new OperationResult<TR>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<TR>() { Success = false, Exception = e };
            }
        }
        protected async Task<OperationResult<TR>> ExecuteOperationAsync<TR>(Func<Task<TR>> operation)
        {
            try
            {
                var res = await operation.Invoke();
                return new OperationResult<TR>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<TR>() { Success = false, Exception = e };
            }
        }
    }
}
