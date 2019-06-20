using System;

namespace ToDo.Core.SharedKernel
{
	// source: https://github.com/jhewlett/ValueObject
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class IgnoreMemberAttribute : Attribute
	{
	}
}