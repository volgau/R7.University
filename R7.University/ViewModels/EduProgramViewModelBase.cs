//
//  EduProgramViewModelBase.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016 Roman M. Yagodin
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
using R7.University.Models;
using System.Collections.Generic;

namespace R7.University.ViewModels
{
    public abstract class EduProgramViewModelBase: IEduProgram
    {
        public IEduProgram EduProgram { get; protected set; }

        protected EduProgramViewModelBase (IEduProgram model)
        {
            EduProgram = model;
        }

        #region IEduProgram implementation

        public int EduProgramID => EduProgram.EduProgramID;

        public int EduLevelID => EduProgram.EduLevelID;

        public string Code => EduProgram.Code;

        public string Title => EduProgram.Title;

        public string Generation => EduProgram.Generation;

        public string HomePage => EduProgram.HomePage;

        public DateTime? StartDate => EduProgram.StartDate;

        public DateTime? EndDate => EduProgram.EndDate;

        public int LastModifiedByUserID => EduProgram.LastModifiedByUserID;

        public DateTime LastModifiedOnDate => EduProgram.LastModifiedOnDate;

        public int CreatedByUserID => EduProgram.CreatedByUserID;

        public DateTime CreatedOnDate => EduProgram.CreatedOnDate;

        public EduLevelInfo EduLevel => EduProgram.EduLevel;

        public ICollection<DocumentInfo> Documents => EduProgram.Documents;

        public ICollection<EduProgramProfileInfo> EduProgramProfiles => EduProgram.EduProgramProfiles;

        public ICollection<EduProgramDivisionInfo> Divisions => EduProgram.Divisions;

        public ICollection<ScienceRecordInfo> ScienceRecords => EduProgram.ScienceRecords;

        public IScience Science => EduProgram.Science;

        #endregion
    }
}
