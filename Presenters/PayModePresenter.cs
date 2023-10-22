using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
            CleanViewFields();
        }

        private void SavePayMode(object? sender, EventArgs e)
        {
            var payMode = new PayModeModel();
            payMode.Id = Convert.ToInt32(view.PayModeId);
            payMode.Name = view.PayModeName;
            payMode.Observation = view.PayModeObservation;

            try
            {
                new Common.ModelDataValidation().Validate(payMode);
                if(view.IsEdit)
                {
                    repository.Edit(payMode);
                    view.Message = "PayMode edited successfuly";
                }
                else
                {
                    repository.Add(payMode);
                    view.Message = "PayMode added successfuly";
                }


                view.IsSuccessfull = true;
                loadAllPayModeList();
                CleanViewFields();
            }

            catch(Exception ex)
            {

                view.IsSuccessfull = false;
                view.Message = ex.Message;
            }


        }

        private void CleanViewFields()
        {

            view.PayModeId = "0";
            view.PayModeName = "";
            view.PayModeObservation = "";


        }

        

        

        private void LoadSelectPayModeToEdit(object? sender, EventArgs e)
        {

            var PayMode = (PayModeModel)payModeBindingSource.Current;


            view.PayModeId = PayMode.Id.ToString();
            view.PayModeName = PayMode.Name;
            view.PayModeObservation = PayMode.Observation;

            view.IsEdit = true;


        }

        private void DeleteSelectedPayMode(object? sender, EventArgs e)
        {
            try
            {


                var payMode = (PayModeModel)payModeBindingSource.Current;

                repository.Delete(payMode.Id);
                view.IsSuccessfull = true;
                view.Message = "Pay Mode deleted successfully";
                loadAllPayModeList();
                    
            }

            catch (Exception ex) 
            {
                view.IsSuccessfull = false;
                view.Message = "An error ocurred, could not delete pay mode";
            
            }
        }

        private void AddNewPayMode(object? sender, EventArgs e)
        {

            view.IsEdit = false;

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
