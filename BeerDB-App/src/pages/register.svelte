<script>
    import Header from './slots/header.svelte'
    import axios from 'axios'

    let username = "";
    let password = "";
    let confirm_password = "";

    let checkPswCorrect = function() {
      if (document.getElementById('password').value ==
        document.getElementById('confirm_password').value) {
        document.getElementById('pswCorrectMessage').style.color = 'green';
        document.getElementById('pswCorrectMessage').innerHTML = 'passwords matching';
        axios({
          method: 'post',
          url: "http://localhost:50698/api/auth/register",
          data: {
            username: username,
            password: password
          }
        })
        .then(function(response){
              console.log(response);
              window.location='./login'
        })
        .catch(err => console.log(err))
      } else {
        document.getElementById('pswCorrectMessage').style.color = 'red';
        document.getElementById('pswCorrectMessage').innerHTML = 'passwords not matching';

      }
    }

    function checkPswStrength() {
      let psw = document.getElementById('password');
      let strengthMessage = document.getElementById('pswStrengthMessage');
      let lowerCaseLetters = /[a-z]/g;
      let upperCaseLetters = /[A-Z]/g;
      let numbers = /[0-9]/g;
      if(psw.value.match(lowerCaseLetters,upperCaseLetters,numbers) && psw.value.length >= 8) {  
        strengthMessage.innerHTML = ""
      } else {
        strengthMessage.innerHTML = "Password not strong enough!"
      }
    }    
</script>

<main>
    <Header headerMessage={"Register"} logoImage="beer.png"></Header>

    <form  on:submit|preventDefault={checkPswStrength} on:submit|preventDefault={checkPswCorrect}>

        <div class="registerForm">
          <div>
            <label for="uname"><b>Username</b></label>
            <input bind:value={username} type="text" placeholder="Enter Username" name="uname" required>
          </div>
          <div>
            <label for="psw"><b>Password (must have symbol(s) & number(s))</b></label>
            <input bind:value={password} id="password" type="password" placeholder="Enter Password" name="psw" required>
          </div>
          <div>
            <label for="conf_psw"><b>Confirm password</b></label>
            <input bind:value={confirm_password} id="confirm_password" type="password" placeholder="Confirm Password" name="conf_psw" required>
          </div>

          <button type="submit">Register</button>
          <p id="pswCorrectMessage"></p>
          <p id="pswStrengthMessage"></p>
        </div>
    </form>
</main>

<style>
  .registerForm {
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    align-content: flex-start;
  }

  input[type=text], input[type=password] {
    width: 500px;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
  }

  button[type=submit]{
    width: 500px;
    padding: 12px 20px;
    margin: 8px 0;
    background-color: green;
    color: white;
    border: none;
    cursor: pointer;
  }

  button[type=submit]:hover {
    opacity: 0.8;
  }
</style>