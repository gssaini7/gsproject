 public class BlogTypeService : iBlogTypeService
    {
        private readonly cUnitOfWork _unitOfWork;

        public BlogTypeService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(BlogTypeModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.BlogTypeRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.BlogTypeModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogTypeModel> GetAll()
        {
            var results = _unitOfWork.BlogTypeRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public BlogTypeModel GetById(Guid tid)
        {
            var result = _unitOfWork.BlogTypeRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(BlogTypeModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.BlogTypeRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, BlogTypeModel tentity)
        {
            throw new NotImplementedException();
        }
    }

