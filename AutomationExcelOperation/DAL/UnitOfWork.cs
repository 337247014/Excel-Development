using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private MyDBEntities _context = null;
        public UnitOfWork()
        {
            _context = new MyDBEntities();
        }

        private GenericRepository<Country> _countryRepository;
        public GenericRepository<Country> CountryRepository
        {
            get
            {
                if (this._countryRepository == null)
                    this._countryRepository = new GenericRepository<Country>(_context);
                return _countryRepository;
            }
        }

        private GenericRepository<Company> _companyRepository;
        public GenericRepository<Company> CompanyRepository
        {
            get
            {
                if (this._companyRepository == null)
                    this._companyRepository = new GenericRepository<Company>(_context);
                return _companyRepository;
            }
        }

        private GenericRepository<ValidationRule> _validationRuleRepository;
        public GenericRepository<ValidationRule> ValidationRuleRepository
        {
            get
            {
                if (this._validationRuleRepository == null)
                    this._validationRuleRepository = new GenericRepository<ValidationRule>(_context);
                return _validationRuleRepository;
            }
        }

        private GenericRepository<WebChatLink> _webChatLinkRepository;
        public GenericRepository<WebChatLink> WebChatLinkRepository
        {
            get
            {
                if (this._webChatLinkRepository == null)
                    this._webChatLinkRepository = new GenericRepository<WebChatLink>(_context);
                return _webChatLinkRepository;
            }
        }

        private GenericRepository<Product> _productRepository;
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new GenericRepository<Product>(_context);
                return _productRepository;
            }
        }

        private GenericRepository<User> _userRepository;
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<User>(_context);
                return _userRepository;
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
