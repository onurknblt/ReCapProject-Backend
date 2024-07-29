﻿using Business.Abstract;
using Business.BusinessAspects.AutoFac;
using Business.Constrants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(FVCustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(FVCustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        [CacheAspect(10)]
        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(result, Messages.Listed);
        }

        [CacheAspect(10)]
        public IDataResult<Customer> GetById(int customerId)
        {
            var result = _customerDal.Get(c => c.CustomerId == customerId);

            return new SuccessDataResult<Customer>(result, Messages.Geted);
        }

        [CacheAspect(10)]
        public IDataResult<Customer> GetByUserId(int userId)
        {
            var result = _customerDal.Get(c => c.UserId == userId);
            return new SuccessDataResult<Customer>(result, Messages.Geted);
        }
    }
}