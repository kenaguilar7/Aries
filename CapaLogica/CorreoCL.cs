using CapaDatos.Daos;
using CapaEntidad.Entidades.Usuarios;
using CapaEntidad.Enumeradores;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogica
{
    public class CorreoCL
    {
        CorreoDao correoDao = new CorreoDao();
        public DataTable Get()
        {

            return correoDao.Get();
        }
        public Boolean Insert(UsuarioTemporal usuarioTemporal)
        {
            return correoDao.Insert(usuarioTemporal);
        }

        public IEnumerable<UsuarioTemporal> SendMail(IEnumerable<UsuarioTemporal> usuariosTemporales)
        {
            var rejecteds = new List<UsuarioTemporal>();
            foreach (var user in usuariosTemporales)
            {

                if (!Push(user))
                {
                    rejecteds.Add(user);
                    user.EstadoEnvio = EstadoEnvio.Fallido;
                }
                else
                {
                    user.EstadoEnvio = EstadoEnvio.Correcto;
                }

                Insert(user);
            }
            return rejecteds;
        }
        public bool Push(UsuarioTemporal usuario)
        {
            try
            {
                //ToDo
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
