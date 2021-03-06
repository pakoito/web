﻿//  Copyright 2012 
//  Name: Ryan Williams
//  URL: http://ryanmichaelwilliams.com | http://dasklub.com

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

namespace BootBaronLib.Interfaces
{
    /// <summary>
    /// Reflects that this object can get every row in the table without
    /// any filtering
    /// </summary>
    public interface IGetAll
    {
        /// <summary>
        /// Get every row in the table
        /// </summary>
        void GetAll();
    }
}
