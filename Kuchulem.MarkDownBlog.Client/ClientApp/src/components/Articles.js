import React, { Component } from 'react';

export class Articles extends Component {
    static displayNAme = Articles.name;

    constructor(props) {
        super(props);
        this.state = { articles: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderArticleList(articles) {
        return (
            <div>
                {articles.map(article =>
                    <div class="card article-preview">
                        <img src={article.thumbnail} class="card-img-top" alt={article.title} />
                        <div class="card-body">
                            <h5 class="card-title">{article.title}</h5>
                            <p class="card-text">{article.summary}</p>
                            <a href={"/Articles/" + article.slug} class="btn btn-primary">Read more</a>
                        </div>
                    </div>
                )}
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Articles.renderArticleList(this.state.articles);

        return (
            <div>
                <h1 id="tabelLabel" >Last articles</h1>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('api/articles');
        const data = await response.json();
        this.setState({ articles: data, loading: false });
    }
}