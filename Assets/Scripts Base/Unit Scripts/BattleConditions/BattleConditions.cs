public abstract class BattleConditions
{
    public int duração;
    public ConditionData data;

    public string Nome => data.nome;
    public float ValorEfeito;

    public BattleConditions(ConditionData data)
    {
        this.data = data;
        duração = data.duracaoBase;
    }

    public virtual void AoAplicar(Unidade unidade) {}
    public virtual void InicioDoTurno(Unidade unidade) {}
    public virtual void AoRemover(Unidade unidade) {}
    public virtual void AplicarVisual(Unidade unidade) {}
}