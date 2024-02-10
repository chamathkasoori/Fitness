﻿namespace Fitness.Core.Common;
public class EmailModel
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string? AttachmentName { get; set; }
    public byte[]? Attachment { get; set; }
}