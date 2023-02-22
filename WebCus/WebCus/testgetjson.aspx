<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="testgetjson.aspx.cs" Inherits="WebCus.testgetjson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderTitle" runat="server">
<style>
  
    .proinfo
    {  font-size: 15px;
    line-height: 30px;
    padding: 10px;
        
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        Nhập số HAWB/HBL
        <asp:TextBox ID="txt_idkey" runat="server"></asp:TextBox>
        <asp:Button OnClick="click_check" runat="server" ID="butcheck" Text="Check" />
      
    </center>

       <div class="container">
            <div class="component" style="width: 100%;">
            <div class="product" style="margin-bottom: 10px;">
                    <h3><span>Thông Tin</span></h3>
                    
                </div>
                <div class="proinfo">
                  <div>
           <asp:Label ID="lbl1" runat="server">Loại Bill / BLType:</asp:Label> 
            <asp:Label ID="lbl_type" runat="server"></asp:Label></div>
        <div>
           <asp:Label ID="Label1" runat="server">Số Bill / BLNo:</asp:Label> 
            <asp:Label ID="lbl_no" runat="server"></asp:Label></div>
       
        <div>
            
           <asp:Label ID="Label2" runat="server">Số kiện / Pieces:</asp:Label> 
            <asp:Label ID="lbl_pie" runat="server"></asp:Label></div>
       
        <div>
       
             <asp:Label ID="Label3" runat="server">Tổng trọng lượng /Gross weight(GW):</asp:Label> 
            <asp:Label ID="lbl_gw" runat="server"></asp:Label></div>
       
        <div>
           
             <asp:Label ID="Label5" runat="server">Số Conts /Conts:</asp:Label> 
            <asp:Label ID="lbl_cont" runat="server"></asp:Label></div>
       
        <div>
           
              <asp:Label ID="Label4" runat="server">Số Cbm /Cbm:</asp:Label> 
            <asp:Label ID="lbl_cbm" runat="server"></asp:Label></div>
      
                </div>

                <div class="product" style="margin-bottom: 10px;">
                    <h3><span>Lịch sử vận chuyển</span></h3>
                    
                </div>
                <div class="mc-detail">
                    <div id="divCartInfo" runat="server" class="">

    <table class="table table-bordered  carts" style="width:100%;">
        <%----%>
        <thead>
            <tr>
                <th>
                    STT
                </th>
                <th>
                    BlStatus
                </th>
                <th class="FlighNo_th">
                    FlighNo
                </th>
                <th >
                    Conts
                </th>
                <th>
                    Gross weight
                </th>
                <th>
                    Pieces
                </th>
                <th>
                    Measurement
                </th>
                <th>
                    Airport
                </th>
                <th>
                    TrackDate
                </th>
                <th>
                    Actual
                </th>
                <th>
                    Vessel
                </th>
                <th>
                    Voyage
                </th>
                <th>
                    Location
                </th>
              
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rep" runat="server">
                <ItemTemplate>
                    <tr style="width:100%;">
                        <td scope="row" class="cart_stt">
                            <%#Container.ItemIndex + 1%>
                        </td>
                        <td class="product-price" style="">
                            <%#Eval("BlStatus")%>
                        </td>
                        <td class="FlighNo_td product-price">
                            <%#Eval("FlighNo")%>
                        </td>
                        <td class="product-price" >
                          <p style="width:100px;display:block;float:left;">  <%#Eval("Conts").ToString().Replace(",","<br/>")%></p>
                        </td>
                        <td class="product-price">
                            <%#Eval("Gw")%>
                        </td  >
                          <td class="product-price">
                            <%#Eval("Pieces")%>
                        </td>
                        <td class="product-price">
                            <%#Eval("Measurement")%>
                        </td>
                        <td class="product-price" >
                            <%#Eval("Airport")%>
                        </td>
                        <td class="product-price" style="width:90px;" >
                            <p><%#Eval("TrackDate").ToString().Replace("T","<br/>T")%></p>
                        </td>
                        <td class="product-price" >
                            <%#Eval("Actual")%>
                        </td>
                        <td class="product-price">
                            <%#Eval("Vessel")%>
                        </td>
                        <td class="product-price" >
                            <%#Eval("Voyage")%>
                        </td>
                        <td class="product-price" >
                            <%#Eval("Location")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    </div>
    </div>
    </div>
    </div>
    <script>
    var tex=$('#<%=lbl_type.ClientID %>').val();
    var texUPer=tex.toUpperCase();
    if (texUPer = "AIR") {

        $('.FlighNo_td').hide();
        $('.FlighNo_th').hide();
    
    }
    </script>
</asp:Content>
