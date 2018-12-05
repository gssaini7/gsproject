 public class SiteService : iSiteService
    {
        private readonly cUnitOfWork _unitOfWork;

        public SiteService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(SiteModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.SiteRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.SiteModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SiteModel> GetAll()
        {
            var results = _unitOfWork.SiteRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public SiteModel GetById(Guid tid)
        {
            var result = _unitOfWork.SiteRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(SiteModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.SiteRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, SiteModel tentity)
        {
            throw new NotImplementedException();
        }
    }

