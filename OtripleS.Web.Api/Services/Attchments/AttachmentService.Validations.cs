﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Brokers.DateTimes;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.Attachments;
using OtripleS.Web.Api.Models.Attachments.Exceptions;

namespace OtripleS.Web.Api.Services.Attachments
{
    public partial class AttachmentService : IAttachmentService
    {
        private void ValidateAttachmentId(Guid attachmentId)
        {
            if (IsInvalid(attachmentId))
            {
                throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.Id),
                    parameterValue: attachmentId);
            }
        }

        private void ValidateStorageAttachment(Attachment storageAttachment, Guid attachmentId)
        {
            if (storageAttachment == null)
            {
                throw new NotFoundAttachmentException(attachmentId);
            }
        }

        private bool IsInvalid(Guid input) => input == Guid.Empty;
        private static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
        private static bool IsInvalid(DateTimeOffset input) => input == default;

        private void ValidateAttachmentOnCreate(Attachment attachment)
        {
            ValidateAttachmentIsNull(attachment);
            ValidateAttachmentIdIsNull(attachment.Id);
            ValidateInvalidFields(attachment);
            ValidateInvalidAuditFields(attachment);
            ValidateAuditFieldsDataOnCreate(attachment);
        }

        private void ValidateAuditFieldsDataOnCreate(Attachment attachment)
        {
            switch (attachment)
            {
                case { } when attachment.UpdatedBy != attachment.CreatedBy:
                    throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.UpdatedBy),
                    parameterValue: attachment.UpdatedBy);
            }
        }

        private void ValidateInvalidAuditFields(Attachment attachment)
        {
            switch (attachment)
            {
                case { } when IsInvalid(attachment.CreatedBy):
                    throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.CreatedBy),
                    parameterValue: attachment.CreatedBy);

                case { } when IsInvalid(attachment.CreatedDate):
                    throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.CreatedDate),
                    parameterValue: attachment.CreatedDate);

                case { } when IsInvalid(attachment.UpdatedBy):
                    throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.UpdatedBy),
                    parameterValue: attachment.UpdatedBy);

                case { } when IsInvalid(attachment.UpdatedDate):
                    throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.UpdatedDate),
                    parameterValue: attachment.UpdatedDate);
            }
        }

        private void ValidateInvalidFields(Attachment attachment)
        {
            if (IsInvalid(attachment.Label))
            {
                throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.Label),
                    parameterValue: attachment.Label);
            }

            if (IsInvalid(attachment.Description))
            {
                throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.Description),
                    parameterValue: attachment.Description);
            }

            if (IsInvalid(attachment.ContectType))
            {
                throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.ContectType),
                    parameterValue: attachment.ContectType);
            }

            if (IsInvalid(attachment.Extension))
            {
                throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.Extension),
                    parameterValue: attachment.Extension);
            }
        }

        private void ValidateAttachmentIdIsNull(Guid attachmentId)
        {
            if (attachmentId == default)
            {
                throw new InvalidAttachmentException(
                    parameterName: nameof(Attachment.Id),
                    parameterValue: attachmentId);
            }
        }

        private void ValidateAttachmentIsNull(Attachment attachment)
        {
            if (attachment is null)
            {
                throw new NullAttachmentException();
            }
        }
    }
}