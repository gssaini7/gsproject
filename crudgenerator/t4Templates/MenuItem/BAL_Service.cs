 public class MenuItemService : iMenuItemService
    {
        private readonly cUnitOfWork _unitOfWork;

        public MenuItemService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(MenuItemModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.MenuItemRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.MenuItemModelid;
            }
        }

        public bool Delete(Guid tid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuItemModel> GetAll()
        {
            var results = _unitOfWork.MenuItemRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public MenuItemModel GetById(Guid tid)
        {
            var result = _unitOfWork.MenuItemRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(MenuItemModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.MenuItemRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, MenuItemModel tentity)
        {
            throw new NotImplementedException();
        }
    }

