<script>
    import Header from './slots/header.svelte'
    import axios from 'axios'

    let username = "";
    let password = "";

    function register() {
      axios({
        method: 'post',
        url: "http://localhost:50698/api/auth/register",
        data: {
          username: username,
          password: password
        }
      })
      .then(function(response){
          console.log(response)
      })
      .catch(err => console.log(err.response))
    }

    function checkIfPswCorrect(form){
        password1 = form.psw.value; 
        password2 = form.confirm_psw.value;
        if(password1 != password2){
            alert("passwords did not match: please try again...");
            return false;
        }
        else {
            return true;
        }
    }

    // let pswInput =  document.getElementById('psw');
    // pswInput.onkeyup = function() {
    //     var lowerCaseLetters = /[a-z]/g;
    //     var upperCaseLetters = /[A-Z]/g;
    //     var numbers = /[0-9]/g;
    // }
</script>

<main>
    <Header></Header>
    <h2>Register</h2>

    <form on:submit|preventDefault={register}>

        <div class="container">
            <label for="uname"><b>Username</b></label>
            <input bind:value={username} type="text" placeholder="Enter Username" name="uname" required>

            <label for="psw"><b>Password (must have symbol(s) & number(s))</b></label>
            <input bind:value={password} type="password" placeholder="Enter Password" name="psw" required>

            <label for="psw"><b>Confirm password</b></label>
            <input id="psw" type="password" placeholder="Confirm Password" name="confirm_psw" required>
            
            <label>
            <button type="submit">Register</button>
            <p id="demo"></p>
        </div>
    </form>
</main>

<style>

</style>