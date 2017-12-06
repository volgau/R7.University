﻿//
//  SciencRecordTypeMapping.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using R7.University.Models;

namespace R7.University.Data.Mappings
{   
    public class ScienceRecordMapping: EntityTypeConfiguration<ScienceRecordInfo>
    {
        public ScienceRecordMapping ()
        {
            ToTable (UniversityMappingHelper.GetTableName<ScienceRecordInfo> ());
            HasKey (m => m.ScienceRecordId);
            Property (m => m.ScienceRecordId).HasDatabaseGeneratedOption (DatabaseGeneratedOption.Identity);
            Property (m => m.ScienceRecordId).IsRequired ();
            Property (m => m.ScienceRecordTypeId).IsRequired ();
            Property (m => m.EduProgramId).IsRequired ();
            Property (m => m.Description).IsOptional ();
            Property (m => m.Value1).IsOptional ();
            Property (m => m.Value2).IsOptional ();

            HasRequired (m => m.ScienceRecordType).WithMany ().HasForeignKey (m => m.ScienceRecordTypeId);
        }
    }
}
