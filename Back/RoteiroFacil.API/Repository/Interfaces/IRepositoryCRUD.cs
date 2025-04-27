using RoteiroFacil.API.Context;

namespace RoteiroFacil.API.Repository.Interfaces
{
    public interface IRepositoryCRUD<model>
    {
        public Task<bool> Create(model obj);
        public Task<bool> Update(model obj);
        public Task<bool> Delete(model obj);
        public Task<List<model>> SearchObj(model obj);
        public Task<model> GetId(int id);

    }
}
