//
//  EduProgram.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2015-2017 Roman M. Yagodin
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

using System;
using System.Collections.Generic;

namespace R7.University.Models
{
    public interface IEduProgram: ITrackableEntity
    {
        int EduProgramID { get; }

        int EduLevelID { get; }

        string Code { get; }

        string Title { get; }

        string Generation { get; }

        string HomePage { get; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }

        EduLevelInfo EduLevel { get; }

        ICollection<DocumentInfo> Documents { get; }

        ICollection<EduProgramProfileInfo> EduProgramProfiles { get; }

        ICollection<EduProgramDivisionInfo> Divisions { get; }

        ICollection<ScienceRecordInfo> ScienceRecords { get; }
    }

    public interface IEduProgramWritable: IEduProgram, ITrackableEntityWritable
    {
        new int EduProgramID { get; set; }

        new int EduLevelID { get; set; }

        new string Code { get; set; }

        new string Title { get; set; }

        new string Generation { get; set; }

        new string HomePage { get; set; }

        new DateTime? StartDate { get; set; }

        new DateTime? EndDate { get; set; }

        new EduLevelInfo EduLevel { get; set; }

        new ICollection<DocumentInfo> Documents { get; set; }

        new ICollection<EduProgramProfileInfo> EduProgramProfiles { get; set; }

        new ICollection<EduProgramDivisionInfo> Divisions { get; set; }

        new ICollection<ScienceRecordInfo> ScienceRecords { get; set; }
    }

    public class EduProgramInfo: IEduProgramWritable
    {
        public int EduProgramID { get; set; }

        public int EduLevelID { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Generation { get; set; }

        public string HomePage { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int LastModifiedByUserID { get; set; }

        public DateTime LastModifiedOnDate { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public virtual EduLevelInfo EduLevel { get; set; }

        public virtual ICollection<DocumentInfo> Documents { get; set; } = new HashSet<DocumentInfo> ();

        public virtual ICollection<EduProgramProfileInfo> EduProgramProfiles { get; set; } = new HashSet<EduProgramProfileInfo> ();

        public virtual ICollection<EduProgramDivisionInfo> Divisions { get; set; } = new HashSet<EduProgramDivisionInfo> ();

        public virtual ICollection<ScienceRecordInfo> ScienceRecords { get; set; } = new HashSet<ScienceRecordInfo> ();
    }
}