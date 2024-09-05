using System.Collections.Generic;
using DeckBuilderTutorialC.global;
using Godot;

namespace DeckBuilderTutorialC;

public partial class CardTargetSelector : Node2D
{
    const int ArcPoints = 8;

    Area2D _area2D;
    Line2D _cardArc;

    CardUI _currentCard;
    bool _isTargeting;

    public override void _Ready()
    {
        _area2D = GetNode<Area2D>("Area2D");
        _cardArc = GetNode<Line2D>("CanvasLayer/CardArc");

        _area2D.AreaEntered += OnArea2DAreaEntered;
        _area2D.AreaExited += OnArea2DAreaExited;

        Events.Instance.CardAimStarted += OnCardAimStarted;
        Events.Instance.CardAimEnded += OnCardAimEnded;
    }

    public override void _Process(double delta)
    {
        if (!_isTargeting) return;

        _area2D.Position = GetLocalMousePosition();
        _cardArc.Points = GetPoints();
    }

    Vector2[] GetPoints()
    {
        var points = new List<Vector2>();
        var start = _currentCard.GlobalPosition;
        start = start with { X = start.X + _currentCard.Size.X / 2 };
        var target = GetLocalMousePosition();
        var distance = target - start;

        for (var i = 0; i < ArcPoints; i++)
        {
            var t = 1.0f / ArcPoints * i;
            var x = start.X + distance.X / ArcPoints * i;
            var y = start.Y + EaseOutCubic(t) * distance.Y;
            points.Add(new Vector2(x, y));
        }

        points.Add(target);

        return points.ToArray();
    }

    float EaseOutCubic(float number)
    {
        return 1.0f - Mathf.Pow(1.0f - number, 3.0f);
    }

    void OnArea2DAreaEntered(Area2D area)
    {
        if (_currentCard == null || !_isTargeting) return;

        if (!_currentCard.Targets.Contains(area))
        {
            _currentCard.Targets.Add(area);
        }
    }

    void OnArea2DAreaExited(Area2D area)
    {
        if (_currentCard == null || !_isTargeting) return;

        _currentCard.Targets.Clear();
    }

    void OnCardAimStarted(CardUI cardUI)
    {
        if (!cardUI.Card.IsSingleTargeted()) return;

        _isTargeting = true;
        _area2D.Monitoring = true;
        _area2D.Monitorable = true;
        _currentCard = cardUI;
    }

    void OnCardAimEnded(CardUI cardUI)
    {
        _isTargeting = false;
        _cardArc.ClearPoints();
        _area2D.Position = Vector2.Zero;
        _area2D.Monitoring = false;
        _area2D.Monitorable = false;
        _currentCard = null;
    }
}
