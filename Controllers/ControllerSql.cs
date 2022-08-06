using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TesteBancoDados.Controllers
{
    public class ControllerSql : ControllerBase
    {

        [HttpPost("adicionar")]
        public async Task Adicionar([FromBody]ClienteModel pCliente)
        {
            MyDBContext dBContext = new MyDBContext();

            await dBContext.AddAsync(pCliente);
            await dBContext.SaveChangesAsync();
            
        }

        [HttpPost("alterar")]
        public async Task Alterar([FromBody] ClienteModel pCliente)
        {
            if (pCliente.Id > 0)
            {
                MyDBContext dBContext = new MyDBContext();
                var pEntity = dBContext.Clientes.Where(i => i.Id == pCliente.Id).FirstOrDefault();
                if (pEntity != null)
                {
                    pEntity.Funcao = pCliente.Funcao;
                    pEntity.CPF = pCliente.CPF; 
                    pEntity.Nome = pCliente.Nome;

                    await dBContext.SaveChangesAsync();
                }
            }
        }

        [HttpDelete("deletar/{pId}")]
        public async Task Deletar(int pId)
        {
            if (pId > 0)
            {
                MyDBContext dBContext = new MyDBContext();
                var pEntity = dBContext.Clientes.Where(i => i.Id == pId).FirstOrDefault();
                if (pEntity != null)
                {
                    dBContext.Remove(pEntity);
                    await dBContext.SaveChangesAsync();
                }
            }
        }

        [HttpGet("buscarCliente/{pId}")]
        public async Task<ClienteModel> BuscarCliente(int pId)
        {
            if (pId > 0)
            {
                MyDBContext dBContext = new MyDBContext();
                var pEntity = dBContext.Clientes.Where(i => i.Id == pId).FirstOrDefault();
                if (pEntity != null)
                    return pEntity;
                
            }

            return null;
        }

        [HttpGet("teste")]
        public async Task<string> Mensagem()
        {
            MyDBContext dBContext = new MyDBContext();
            ClienteModel model = new ClienteModel();
            model.Nome = "alex";
            model.CPF = "00000000000"; //11 digitos
            model.Funcao = "dev";

            await dBContext.AddAsync(model);
            await dBContext.SaveChangesAsync();

            return "funcionou";
        }
    }
}
