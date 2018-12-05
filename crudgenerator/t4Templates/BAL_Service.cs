 public class MenuService : iMenuService
    {
        private readonly cUnitOfWork _unitOfWork;

        public MenuService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(MenuModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.MenuRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.MenuModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuModel> GetAll()
        {
            var results = _unitOfWork.MenuRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public MenuModel GetById(Guid tid)
        {
            var result = _unitOfWork.MenuRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(MenuModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.MenuRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, MenuModel tentity)
        {
            throw new NotImplementedException();
        }
    }

