﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using OtripleS.Web.Api.Models.AssignmentAttachments;

namespace OtripleS.Web.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<AssignmentAttachment> InsertAssignmentAttachmentAsync(
            AssignmentAttachment assignmentEntryAttachment);

        public IQueryable<AssignmentAttachment> SelectAllAssignmentAttachments();

        public ValueTask<AssignmentAttachment> SelectAssignmentAttachmentByIdAsync(
            Guid assignmentId,
            Guid attachmentId);

        public ValueTask<AssignmentAttachment> UpdateAssignmentAttachmentAsync(
            AssignmentAttachment assignmentEntryAttachment);

        public ValueTask<AssignmentAttachment> DeleteAssignmentAttachmentAsync(
            AssignmentAttachment assignmentEntryAttachment);
    }
}
