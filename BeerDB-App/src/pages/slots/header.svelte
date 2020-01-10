<slot>
    <div id="header">
        <div class="logo">
            <p>logo</p>
        </div>
        <div class="menu">
            <ul class="menu__list">
                <li class="menu__list-item"><a href="/beers">Bieren</a></li>
                <li class="menu__list-item"><a href="/breweries">Brouwerijen</a></li>
            </ul>
        </div>
    </div>
</slot>

<style>
	#header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 2%;
        background-color: gray;
    }
    .logo {

    }
    .menu {
    }
    .menu__list {
        list-style-type: none;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .menu__list-item {
        margin: 0 10px;
    }
</style>