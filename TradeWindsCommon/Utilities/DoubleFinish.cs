
// Copyright (c) 2024 Trade Winds Studios (David Thielen)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace TradeWindsCommon.Utilities
{
	/// <summary>
	/// Used when there are two methods and you need to know when the 2nd method completes.
	/// Created to use at the end of OnInitializedAsync() and OnAfterRenderAsync(firstRender ==
	/// true) to call some code that requires that both of these have run.<br/>
	/// For those unfamiliar with Blazor, either one of these events can be called first
	/// and regardless of the call order, if there are tasks called in each, either one
	/// can complete first.<br/>
	/// Only one of the Try methods will return true. The other will return false. And the
	/// one that returns true will only do so once. Subsequent call will return false.
	/// </summary>
	public class DoubleFinish
	{
		private bool _firstFinished = false;
		private bool _secondFinished = false;
		private bool _alreadyReturnedTrue = false;
		private readonly object _padlock = new();

		/// <summary>
		/// Call when the first method completes. Returns true if the 2nd method is also
		/// complete and so this method can now run the code that requires both methods.
		/// </summary>
		public bool TryFirstFinished
		{
			get
			{
				lock (_padlock)
				{
					_firstFinished = true;
					if (_alreadyReturnedTrue || ! _secondFinished)
						return false;
					_alreadyReturnedTrue = true;
					return true;
				}
			}
		}

		/// <summary>
		/// Call when the second method completes. Returns true if the 1st method is also
		/// complete and so this method can now run the code that requires both methods.
		/// </summary>
		public bool TrySecondFinished
		{
			get
			{
				lock (_padlock)
				{
					_secondFinished = true;
					if (_alreadyReturnedTrue || ! _firstFinished)
						return false;
					_alreadyReturnedTrue = true;
					return true;
				}
			}
		}
	}
}
