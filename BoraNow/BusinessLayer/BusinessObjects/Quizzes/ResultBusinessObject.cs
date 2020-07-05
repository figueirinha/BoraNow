using Recodme.RD.BoraNow.BusinessLayer.OperationResults;
using Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes
{
    public class ResultBusinessObject
    {
        private ResultDataAccessObject _dao = new ResultDataAccessObject();
        public ResultBusinessObject()
        {
            _dao = new ResultDataAccessObject();
        }


        #region List
        public OperationResult<List<Result>> List()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = _dao.List();
                    ts.Complete();
                    return new OperationResult<List<Result>>() { Success = true, Result = result };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Result>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Result>>> ListAsync()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await _dao.ListAsync();
                    ts.Complete();
                    return new OperationResult<List<Result>>() { Success = true, Result = result };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<Result>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Count
        public OperationResult<int> CountAll()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = _dao.List().Count;
                    ts.Complete();
                    return new OperationResult<int>() { Success = true, Result = result };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<int>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<int>> CountAllAsync()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await _dao.ListAsync();
                    ts.Complete();
                    return new OperationResult<int>() { Success = true, Result = result.Count };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<int>() { Success = false, Exception = e };
            }
        }
        #endregion 

        #region Create
        public OperationResult Create(Result result)
        {
            try
            {

                _dao.Create(result);
                return new OperationResult() { Success = true };

            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> CreateAsync(Result result)
        {
            try
            {
                await _dao.CreateAsync(result);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read
        public OperationResult<Result> Read(Guid id)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = _dao.Read(id);
                    transactionScope.Complete();
                    return new OperationResult<Result>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<Result>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<Result>> ReadAsync(Guid id)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _dao.ReadAsync(id);
                    transactionScope.Complete();
                    return new OperationResult<Result>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<Result>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public OperationResult Update(Result result)
        {
            try
            {
                _dao.Update(result);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> UpdateAsync(Result result)
        {
            try
            {
                await _dao.UpdateAsync(result);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }
        #endregion

        #region Delete
        public OperationResult Delete(Result result)
        {
            try
            {
                _dao.Delete(result);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }
        public async Task<OperationResult> DeleteAsync(Result result)
        {
            try
            {
                await _dao.DeleteAsync(result);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }

        public OperationResult Delete(Guid id)
        {
            try
            {
                _dao.Delete(id);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }
        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            try
            {
                await _dao.DeleteAsync(id);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }

        #endregion
    }
}
