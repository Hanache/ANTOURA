using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ANTOURA.Models
{
   
        #region Models

        [MetadataType(typeof(ExamenValidation))]
        public partial class Examen
        {

        }

        public partial class ExamenValidation
        {
            [Required]
            public string title { get; set; }
            public string overGrade { get; set; }
            
        }
        #endregion

        #region Repository
        public class ExamenRepository
        {
            private DataClassesDataContext dc = new DataClassesDataContext();

            #region Create
            public void Add(Examen entry)
            {
                dc.Examens.InsertOnSubmit(entry);
            }
            #endregion

            #region Read
            public Examen GetById(int? id)
            {
                return dc.Examens.FirstOrDefault(d => d.id == id);
            }

            public IQueryable<Examen> GetAll()
            {
                return dc.Examens;
            }

            //public IQueryable<JsonExamen> GetAllAsJson()
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
            public void Delete(Examen entry)
            {
                dc.Examens.DeleteOnSubmit(entry);
            }
            #endregion

        }
        #endregion
     
    }
