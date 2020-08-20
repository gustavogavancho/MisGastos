using FluentValidation;
using FluentValidation.Results;
using LiteDB;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace MisGastos.DAL.Local.LiteDB
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDTO
    {
        string _tableName;
        LiteDatabase _db;
        private AbstractValidator<T> _validator;

        public GenericRepository(AbstractValidator<T> validator)
        {
            _validator = validator;
            _tableName = typeof(T).Name;
            _db = new LiteDatabase(DependencyService.Get<IDBPath>().DBDir());
        }

        private void VerificarSiLaDireccionExiste(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private LiteCollection<T> Collection() => _db.GetCollection<T>(_tableName);

        public string Error { get; private set; }

        public IEnumerable<T> Read
        {
            get
            {
                try
                {
                    return Collection().FindAll();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<string> DatosNoTomarEnCuenta { get; set; }

        public T Create(T entidad)
        {
            ValidationResult validationResult = _validator.Validate(entidad);
            if (validationResult.IsValid)
            {
                entidad.Id = ObjectId.NewObjectId().ToString();
                entidad.FechaHoraCreacion = DateTime.Now;
                try
                {
                    Collection().Insert(entidad);
                    this.Error = "";
                    return entidad;
                }
                catch (Exception ex)
                {
                    this.Error = ex.Message;
                    return null;
                }
            }
            else
            {
                this.Error = "Error de validación\n ";
                foreach (ValidationFailure item in validationResult.Errors)
                {
                    this.Error += $"{item.ErrorMessage}\n\r";
                }
                return null;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                return Collection().Delete(e => e.Id.ToString() == id) > 0;
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                return false;
            }
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicado) => this.Read.Where(predicado.Compile());

        public T SearchById(string id) => this.Read.Where(e => e.Id.ToString() == id).SingleOrDefault();

        public T Update(T entidad)
        {
            ValidationResult validationResult = _validator.Validate(entidad);
            if (validationResult.IsValid)
            {
                try
                {
                    Collection().Update(entidad);
                    this.Error = "";
                    return entidad;
                }
                catch (Exception ex)
                {
                    this.Error = ex.Message;
                    return null;
                }

            }
            else
            {
                this.Error = "Error de validación: ";
                foreach (ValidationFailure item in validationResult.Errors)
                {
                    this.Error += $"{item.ErrorMessage}.\n\r";
                }
                return null;
            }
        }
    }
}
