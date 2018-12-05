 public class ClassService : iClassService
    {
        private readonly cUnitOfWork _unitOfWork;

        public ClassService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(ClassModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.ClassRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.ClassModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClassModel> GetAll()
        {
            var results = _unitOfWork.ClassRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public ClassModel GetById(Guid tid)
        {
            var result = _unitOfWork.ClassRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(ClassModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.ClassRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, ClassModel tentity)
        {
            throw new NotImplementedException();
        }
    }

