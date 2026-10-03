using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Application.Base
{
    public class BaseResult<T>
    {

        public T Data { get; set; }
        public List<Error> Errors { get; set; }
        public bool IsSuccessful => !Errors.Any() || Errors == null; //hata listesiiçinde varsa any yoksa !any



        public static BaseResult<T> Success(T data)
        {
            return new BaseResult<T>
            {
                Data = data
            };
        }


        public static BaseResult<T> Fail(List<ValidationFailure> validationErrors)
        {
            var errors = (from error in validationErrors
                          select new Error
                          {
                              ErrorMessage = error.ErrorMessage,
                              PropertyName = error.PropertyName
                          }).ToList();

            return new BaseResult<T>
            {
                Errors = errors
            };



        }


        public static BaseResult<T> Fail(List<IdentityError> identityErrors)
        {
            var errors = (from error in identityErrors
                          select new Error
                          {
                              ErrorMessage = error.Description,
                              PropertyName = error.Code
                          }).ToList();

            return new BaseResult<T>
            {
                Errors = errors
            };



        }


        public static BaseResult<T> Fail( string message)
        {
            return new BaseResult<T>
            {
                Errors = new List<Error>
                {
                    new Error
                    {
                        ErrorMessage = message,
                        PropertyName = string.Empty
                    }
                }
            };
        }





    }

    public class Error
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }


}
