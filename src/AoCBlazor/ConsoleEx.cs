using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text;

public class ConsoleEx
{
    ConsoleColor currentColor;
    ReusableAwaiter<string?> source;

    StringBuilder markup;

    public event EventHandler LineAdded;

    public ConsoleColor ForegroundColor
    {
        get => currentColor;
        set => currentColor = value;
    }

    public MarkupString Markup => new MarkupString(markup.ToString());

    public ConsoleEx()
    {
        source = new ReusableAwaiter<string?>();
        currentColor = ConsoleColor.Gray;

        markup = new StringBuilder(65536);
    }

    public void Write(string? text, ConsoleColor? color = null)
    {
        markup.Append($"<span class=\"{color ?? currentColor}\">{text}</span>");
    }

    public void Write(object value, ConsoleColor? color = null)
    {
        Write(value?.ToString(), color);
    }

    public void WriteLine(string? text, ConsoleColor? color = null)
    {
        markup.Append($"<span class=\"{color ?? currentColor}\">{text}</span>\r\n");
        LineAdded?.Invoke(this, EventArgs.Empty);
    }

    public void WriteLine(object value, ConsoleColor? color = null)
    {
        WriteLine(value?.ToString(), color);
    }

    public void WriteLine()
    {
        WriteLine("");
    }

    public async Task<string> ReadLine()
    {
        var ret = await source;
        _ = source.Reset();
        return ret ?? string.Empty;
    }

    public void PostReadLine(string? input)
    {
        source.TrySetResult(input);

    }

    public sealed class ReusableAwaiter<T> : INotifyCompletion
    {
        private Action _continuation = null;
        private T _result = default(T);
        private Exception _exception = null;

        public bool IsCompleted
        {
            get;
            private set;
        }

        public T GetResult()
        {
            if (_exception != null)
                throw _exception;
            return _result;
        }

        public void OnCompleted(Action continuation)
        {
            if (_continuation != null)
                throw new InvalidOperationException("This ReusableAwaiter instance has already been listened");
            _continuation = continuation;
        }

        /// <summary>
        /// Attempts to transition the completion state.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TrySetResult(T result)
        {
            if (!this.IsCompleted)
            {
                this.IsCompleted = true;
                this._result = result;

                if (_continuation != null)
                    _continuation();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempts to transition the exception state.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TrySetException(Exception exception)
        {
            if (!this.IsCompleted)
            {
                this.IsCompleted = true;
                this._exception = exception;

                if (_continuation != null)
                    _continuation();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reset the awaiter to initial status
        /// </summary>
        /// <returns></returns>
        public ReusableAwaiter<T> Reset()
        {
            this._result = default(T);
            this._continuation = null;
            this._exception = null;
            this.IsCompleted = false;
            return this;
        }

        public ReusableAwaiter<T> GetAwaiter()
        {
            return this;
        }
    }
}