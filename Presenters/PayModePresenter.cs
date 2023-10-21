using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket._Repositories;
using Supermarket.Models;
using Supermarket.Views;
namespace Supermarket.Presenters
{
    internal class PayModePresenter
    {
        private IPayModeView view;

        private IPayModeRepository repository;

        private BindingSource payModeBindingSource;

        private IEnumerable<PayModeModel> payModelist;

        public PayModePresenter(IPayModeView view, IPayModeRepository repository)
        {

            this.payModeBindingSource = new BindingSource();

            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchPayMode;
            this.view.SearchEvent += AddNewPayMode;
            this.view.SearchEvent += LoadSelectPayModeToEdit;
            this.view.SearchEvent += DeleteSelectedPayMode;
            this.view.SearchEvent += SavePayMode;
            this.view.SearchEvent += CancelAction;

            this.view.SetPayModeListBildingSource(payModeBindingSource);

            loadAllPayModeList();

            this.view.Show();
        }

        private void loadAllPayModeList()
        {
            payModelist = repository.GetAll();
            payModeBindingSource.DataSource = payModelist;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SavePayMode(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadSelectPayModeToEdit(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteSelectedPayMode(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddNewPayMode(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchPayMode(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue==false)
            {
                payModelist = repository.GetByValue(this.view.SearchValue);
            }
            else
            {
                payModelist = repository.GetAll();
            }
            payModeBindingSource.DataSource = payModelist;
        }
    }
}
