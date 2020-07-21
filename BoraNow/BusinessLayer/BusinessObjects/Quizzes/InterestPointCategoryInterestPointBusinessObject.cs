using Recodme.RD.BoraNow.BusinessLayer.OperationResults;
using Recodme.RD.BoraNow.DataAccessLayer.DataAccessObjects.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes
{
   public class InterestPointCategoryInterestPointBusinessObject
    {
        private InterestPointCategoryInterestPointDataAccessObject _dao;
        public InterestPointCategoryInterestPointBusinessObject()
        {
            _dao = new InterestPointCategoryInterestPointDataAccessObject();
        }

        #region List
        public OperationResult<List<InterestPointCategoryInterestPoint>> List()
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
                    return new OperationResult<List<InterestPointCategoryInterestPoint>>() { Success = true, Result = result };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<InterestPointCategoryInterestPoint>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<InterestPointCategoryInterestPoint>>> ListAsync()
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
                    return new OperationResult<List<InterestPointCategoryInterestPoint>>() { Success = true, Result = result };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<List<InterestPointCategoryInterestPoint>>() { Success = false, Exception = e };
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
        public OperationResult Create(InterestPointCategoryInterestPoint interestPointCategory)
        {
            try
            {

                _dao.Create(interestPointCategory);
                return new OperationResult() { Success = true };

            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> CreateAsync(InterestPointCategoryInterestPoint interestPointCategory)
        {
            try
            {
                await _dao.CreateAsync(interestPointCategory);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read
        public OperationResult<InterestPointCategoryInterestPoint> Read(Guid id)
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
                    return new OperationResult<InterestPointCategoryInterestPoint>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<InterestPointCategoryInterestPoint>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<InterestPointCategoryInterestPoint>> ReadAsync(Guid id)
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
                    return new OperationResult<InterestPointCategoryInterestPoint>() { Success = true, Result = res };
                }
            }
            catch (Exception e)
            {
                return new OperationResult<InterestPointCategoryInterestPoint>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public OperationResult Update(InterestPointCategoryInterestPoint interestPointCategory)
        {
            try
            {
                _dao.Update(interestPointCategory);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> UpdateAsync(InterestPointCategoryInterestPoint interestPointCategory)
        {
            try
            {
                await _dao.UpdateAsync(interestPointCategory);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }
        #endregion

        #region Delete
        public OperationResult Delete(InterestPointCategoryInterestPoint interestPointCategory)
        {
            try
            {
                _dao.Delete(interestPointCategory);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }
        public async Task<OperationResult> DeleteAsync(InterestPointCategoryInterestPoint interestPointCategory)
        {
            try
            {
                await _dao.DeleteAsync(interestPointCategory);
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
