 public class ImageGalleryService : iImageGalleryService
    {
        private readonly cUnitOfWork _unitOfWork;

        public ImageGalleryService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(ImageGalleryModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.ImageGalleryRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.ImageGalleryModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageGalleryModel> GetAll()
        {
            var results = _unitOfWork.ImageGalleryRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public ImageGalleryModel GetById(Guid tid)
        {
            var result = _unitOfWork.ImageGalleryRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(ImageGalleryModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.ImageGalleryRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, ImageGalleryModel tentity)
        {
            throw new NotImplementedException();
        }
    }

