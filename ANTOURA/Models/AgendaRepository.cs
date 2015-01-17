using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ANTOURA.Models
{
   
        #region Models

        [MetadataType(typeof(AgendaValidation))]
        public partial class Agenda
        {

        }

        public partial class AgendaValidation
        {
            [Required]
            public string description { get; set; }
        }
        #endregion

        #region Repository
        public class AgendaRepository
        {
            private DataClassesDataContext dc = new DataClassesDataContext();

            #region Create
            public void Add(Agenda entry)
            {
                dc.Agendas.InsertOnSubmit(entry);
            }
            #endregion

            #region Read
            public Agenda GetById(int? id)
            {
                return dc.Agendas.FirstOrDefault(d => d.id == id);
            }

            public IQueryable<Agenda> GetAll()
            {
                return dc.Agendas;
            }

            //public IQueryable<JsonAgenda> GetAllAsJson()
            //{
            //    return GetAll();
            //}

            #endregion

            #region Update
            public void Save()
            {
                dc.SubmitChanges();
            }
            #endregion

            #region Delete
            public void Delete(Agenda entry)
            {
                dc.Agendas.DeleteOnSubmit(entry);
            }
            #endregion

        }
        #endregion
     
    }
