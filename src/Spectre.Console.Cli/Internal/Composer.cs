namespace Spectre.Console.Cli;

/// <summary>
/// Composer class for composing commands
/// </summary>
internal sealed class Composer : IRenderable
{
    private readonly StringBuilder _content;

    /// <summary>
    /// Initiliazes a new instance of the <see cref="Composer"/> class.
    /// </summary>
    public Composer()
    {
        _content = new StringBuilder();
    }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public Composer Text(string text)
    {
        _content.Append(text);
        return this;
    }

    /// <summary>
    /// Gets or sets the style of the command.
    /// </summary>
    public Composer Style(string style, string text)
    {
        _content.Append('[').Append(style).Append(']');
        _content.Append(text.EscapeMarkup());
        _content.Append("[/]");
        return this;
    }

    /// <summary>
    /// Gets or sets the style with the Composer action. 
    /// </summary>
    public Composer Style(string style, Action<Composer> action)
    {
        _content.Append('[').Append(style).Append(']');
        action(this);
        _content.Append("[/]");
        return this;
    }

    /// <summary>
    /// Gets or sets the Space with a call to Spaces()
    /// </summary>
    public Composer Space()
    {
        return Spaces(1);
    }

    /// <summary>
    /// Gets or sets the "count" amount of space
    /// </summary>
    /// <param name="count">The count.</param>
    public Composer Spaces(int count)
    {
        return Repeat(' ', count);
    }

    /// <summary>
    /// Gets or sets the Tab
    /// </summary>
    public Composer Tab()
    {
        return Tabs(1);
    }

    /// <summary>
    /// Gets or sets the "count" amount of tabs which is just 4 Spaces
    /// </summary>
    /// <param name="count">The count.</param>
    public Composer Tabs(int count)
    {
        return Spaces(count * 4);
    }

    /// <summary>
    /// Repeats the character passed count amount of times
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="count">The count.</param>
    public Composer Repeat(char character, int count)
    {
        _content.Append(new string(character, count));
        return this;
    }

    /// <summary>
    /// Adds a line break.
    /// </summary>
    public Composer LineBreak()
    {
        return LineBreaks(1);
    }

    /// <summary>
    /// Adds a line break.
    /// </summary>
    /// param name="count">The count.</param>
    public Composer LineBreaks(int count)
    {
        for (var i = 0; i < count; i++)
        {
            _content.Append(Environment.NewLine);
        }

        return this;
    }

    /// <summary>
    /// Joins the separator with the composer passed.
    /// </summary>
    /// param name="separator">The separator.</param>
    /// param name="composers">The composer.</param>
    public Composer Join(string separator, IEnumerable<string> composers)
    {
        if (composers != null)
        {
            Space();
            Text(string.Join(separator, composers));
        }

        return this;
    }

    /// <summary>
    /// Measures the context.
    /// </summary>
    /// param name="context">The context.</param>
    /// param name="maxWidth">The maxWidth.</param>
    public Measurement Measure(RenderContext context, int maxWidth)
    {
        return ((IRenderable)new Markup(_content.ToString())).Measure(context, maxWidth);
    }

    /// <summary>
    /// Renders the context.
    /// </summary>
    /// param name="context">The context.</param>
    /// param name="maxWidth">The maxWidth.</param>
    public IEnumerable<Segment> Render(RenderContext context, int maxWidth)
    {
        return ((IRenderable)new Markup(_content.ToString())).Render(context, maxWidth);
    }

    /// <summary>
    /// Type conversion from content to string.
    /// </summary>
    public override string ToString()
    {
        return _content.ToString();
    }
}