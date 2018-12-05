 public class LayoutService : iLayoutService
    {
        private readonly cUnitOfWork _unitOfWork;

        public LayoutService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(LayoutModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.LayoutRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.LayoutModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LayoutModel> GetAll()
        {
            var results = _unitOfWork.LayoutRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public LayoutModel GetById(Guid tid)
        {
            var result = _unitOfWork.LayoutRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(LayoutModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.LayoutRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, LayoutModel tentity)
        {
            throw new NotImplementedException();
        }
    }

