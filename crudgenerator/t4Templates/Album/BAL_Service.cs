 public class AlbumService : iAlbumService
    {
        private readonly cUnitOfWork _unitOfWork;

        public AlbumService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(AlbumModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.AlbumRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.AlbumModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlbumModel> GetAll()
        {
            var results = _unitOfWork.AlbumRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public AlbumModel GetById(Guid tid)
        {
            var result = _unitOfWork.AlbumRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(AlbumModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.AlbumRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, AlbumModel tentity)
        {
            throw new NotImplementedException();
        }
    }

