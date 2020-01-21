<script>
    import { onMount } from 'svelte';
    import axios from 'axios';

    export let headerMessage;
    export let logoImage;
    
    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for(var i = 0; i <ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
            c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    let user = getCookie('loggedIn');

    onMount(function checkIfLoggedIn(){
        if(!user){
            document.getElementById('logout').style.display = "none"
        }
        else {
            document.getElementById('login').style.display = "none"
        }
    })

    function logout() {
      axios({
        method: 'post',
        url: "http://localhost:50698/api/auth/logout",
      })
      .then(function(response){
            console.log(response);

            var cookies = document.cookie.split(";");
            for (var i = 0; i < cookies.length; i++) {
                var cookie = cookies[i];
                var eqPos = cookie.indexOf("=");
                var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
            }
            window.location='../login'
      })
      .catch(err => console.log(err))
    }
</script>

<slot>
    <div class="header">
        <div class="header_menu">
            <a href="/"><div class="logo">
                <img src="{logoImage}" alt="logo">
            </div></a>
            <div class="menu">
                <ul class="menu__list">
                    <li class="menu__list-item"><a href="/beers">Beers</a></li>
                    <li class="menu__list-item"><a href="/breweries">Breweries</a></li>
                    <li id="login" class="menu__list-item button"><a href="/login">Login</a></li>
                    <button id="logout" on:click|preventDefault = {logout}><li class="menu__list-item button"><a href="../login">Logout</a></li></button>
                </ul>
            </div>
        </div>
        <div class="header_extra">
            <h1>{headerMessage}</h1>
        </div>
    </div>
</slot>

<style>
	.header_menu {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 2%;
    }
    .header_extra {
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .header_extra h1 {
        font-size: 48px;
        font-weight: 900;
    }
    .logo img{
        width: 50px;
    }
    .menu__list {
        list-style-type: none;
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 18px;
        font-weight: 600;
    }
    .menu__list a{
        color: rgb(0, 81, 255);
    }
    .menu__list-item {
        margin: 0 30px;
    }
    #logout {
        font-size: 18px;
        font-weight: 600;
        margin: 0;
        padding: 0;
        background-color: transparent;
        border: transparent;
    }
</style>